module Advent.Domain

open System

type Snack =
  | Snack of int

  member x.Value = let (Snack value) = x in value

type Snacks =
  { Items: Snack list
    TotalCaloricValue: int }

type RucksackItem =
  | RucksackItem of char

  member x.Priority =
    let (RucksackItem value) = x
    if Char.IsLower value then
      int value - 96
    else
      (int value - 64) + 26

type Rucksack =
  { CompartmentOne: char Set
    CompartmentTwo: char Set
    FullInventory: string}

  member x.mispackagedItem =
    let intersection = Set.intersect x.CompartmentOne x.CompartmentTwo

    RucksackItem intersection.MaximumElement

  static member private group(packs: Rucksack seq) =
    let inventory =
      packs |> Seq.map (fun x ->  Set(x.FullInventory)) |> Seq.toArray

    let first = Set.intersect inventory[0] inventory[1]
    let second = Set.intersect first inventory[2]

    RucksackItem second.MaximumElement

  static member private parse(input: string seq) =
    seq {
      for line in input do
        let rucksackCompartments = line |> Seq.splitInto 2 |> Seq.toList

        yield
          { CompartmentOne = Set(rucksackCompartments.Head)
            CompartmentTwo = Set(rucksackCompartments[1])
            FullInventory = line }
    }
    
  static member calculatePriority input =
    let rucksacks = Rucksack.parse input
    let priority = rucksacks |> Seq.sumBy (fun x -> x.mispackagedItem.Priority)
    priority
    
  static member calculateGroupPriority input =
    let rucksacks = Rucksack.parse input
    let groups = rucksacks |> Seq.chunkBySize 3
    let priority = groups |> Seq.map Rucksack.group |> Seq.sumBy(fun x -> x.Priority)
    priority

type Elf = { Snacks: Snacks }

type Inventory =
  | Inventory of string

  member x.readSnacksByElf = let (Inventory value) = x in value.Split("\n\n")


type Party =
  { Members: Elf list }

  member x.mostSnacks() =
    x.Members
    |> Seq.sortByDescending (fun x -> x.Snacks.TotalCaloricValue)
    |> Seq.head

  member x.maxSnacksAcrossMultipleElves() =
    x.Members
    |> Seq.sortByDescending (fun x -> x.Snacks.TotalCaloricValue)
    |> Seq.take 3
    |> Seq.sumBy (fun x -> x.Snacks.TotalCaloricValue)

  static member private parseInventory(inventory: Inventory) =
    seq {
      for elf in inventory.readSnacksByElf do
        let load =
          elf.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)

        let caloriesByItem = load |> Seq.map (Int32.Parse)
        let food = caloriesByItem |> Seq.map (Snack)

        yield
          { Snacks =
              { Items = food |> Seq.toList
                TotalCaloricValue = caloriesByItem |> Seq.sum } }
    }

  static member parse(inventory: Inventory) =
    { Members = Party.parseInventory inventory |> Seq.toList }
