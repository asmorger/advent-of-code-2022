module Advent.Cargo

open System
open FParsec

type Crate =
  | Crate of char

  member x.Value = let (Crate value) = x in value

type CrateStack =
  | CrateStack of Crate list

  member x.Value = let (CrateStack value) = x in value

  member x.Last =
    let (CrateStack value) = x in
    value |> ignore
    value |> List.last

  member x.LastN n =
    let (CrateStack value) = x in
    let lastIndex = value.Length - 1
    let sliceIndex = lastIndex - n

    let newValues = value[sliceIndex..lastIndex]
    CrateStack newValues

  member x.DropLast =
    let (CrateStack value) = x in
    value |> ignore
    let newValues = value |> List.removeAt (value.Length - 1)
    CrateStack newValues

  member x.DropLastN n =
    let (CrateStack value) = x in
    let lastIndex = value.Length - 1
    let sliceIndex = lastIndex - n

    let newValues = value[0..sliceIndex]
    CrateStack newValues

  member x.WithValue(crate: Crate) =
    let mutable (CrateStack value) = x in
    value |> ignore

    let newList = [ crate ] |> List.append value
    CrateStack newList

  member x.WithValues crates =
    let mutable (CrateStack value) = x in
    value |> ignore

    let newList = crates |> List.append value
    CrateStack newList

type Instruction =
  { NumberOfItems: int
    FromStack: int
    ToStack: int }

let parseCrate (input: char array) =
  match input with
  | item when item[0] = '[' -> Some(Crate item[1])
  | _ -> None
 
  
let createCrate (input: string) =
  match input with
  | item when input.Length = 1 -> Some(Crate item[0])
  | _ -> None
  
let parseInitialCrates (input: string) =
  let allRows =
    input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)

  let lastIndex = allRows.Length - 2
  let rows = allRows[0..lastIndex]

  let crateDefinitions =
    rows
    |> Seq.map (fun x -> x.ToCharArray())
    |> Seq.map (Seq.chunkBySize 4)
    |> Seq.map Seq.toArray
    |> Seq.toArray

  let columns = crateDefinitions[0].Length - 1

  seq {
    for i = 0 to columns do
      crateDefinitions
      |> Array.map (fun x -> x[i])
      |> Array.map parseCrate
      |> Array.choose id
      |> Array.rev
      |> Array.toList
      |> CrateStack
  }

let createInstruction moves fromStack toStack =
  { NumberOfItems = moves
    FromStack = fromStack - 1
    ToStack = toStack - 1 }

let parseInstruction (input: string) =
  let int_ws = pint32 .>> spaces
  let str_ws s = pstring s .>> spaces

  let moves = str_ws "move" >>. int_ws
  let fromStack = str_ws "from" >>. int_ws
  let toStack = str_ws "to" >>. int_ws

  let instruction = pipe3 moves fromStack toStack createInstruction
  let parser = run instruction input

  match parser with
  | Success (v, _, _) -> v
  | Failure (msg, _, _) -> failwith msg

let parseProcedure (input: string) =
  let source = input.Split("\n\n")
  let startingStackDrawing = source[0]
  let instructionsSource = source[1]

  let stacks = parseInitialCrates startingStackDrawing |> Seq.toArray

  let instructions =
    instructionsSource.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
    |> Array.map parseInstruction

  (stacks, instructions)

let runInstruction (stacks: CrateStack[]) (instruction: Instruction) =
  let mutable finalStacks = stacks
  let mutable fromStack = finalStacks[instruction.FromStack]
  let mutable toStack = finalStacks[instruction.ToStack]

  for i = 1 to instruction.NumberOfItems do
    let crate = fromStack.Value |> List.last
    let newFrom = fromStack.DropLast
    let newTo = toStack.WithValue crate

    finalStacks[instruction.FromStack] <- newFrom
    finalStacks[instruction.ToStack] <- newTo

    fromStack <- newFrom
    toStack <- newTo

  finalStacks

let runInstruction9001 (stacks: CrateStack[]) (instruction: Instruction) =
  let mutable finalStacks = stacks
  let mutable fromStack = finalStacks[instruction.FromStack]
  let mutable toStack = finalStacks[instruction.ToStack]

  let crates = fromStack.LastN (instruction.NumberOfItems - 1)
  let newFrom = fromStack.DropLastN instruction.NumberOfItems
  let newTo = toStack.WithValues crates.Value

  finalStacks[instruction.FromStack] <- newFrom
  finalStacks[instruction.ToStack] <- newTo

  fromStack <- newFrom
  toStack <- newTo

  finalStacks

let executeProcedure (procedure: CrateStack[] * Instruction[]) =
  let mutable stacks, instructions = procedure

  for instruction in instructions do

    let result = runInstruction stacks instruction
    stacks <- result

  stacks |> Array.map (fun x -> x.Last)

let executeProcedure9001 (procedure: CrateStack[] * Instruction[]) =
  let mutable stacks, instructions = procedure

  for instruction in instructions do
    let result = runInstruction9001 stacks instruction
    stacks <- result

  stacks |> Array.map (fun x -> x.Last)
