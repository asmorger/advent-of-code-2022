module Advent.Communications

let isUnique windowSize input =
  let set = Set input
  set.Count = windowSize

let determineFirstUniqueIndex windowSize (input: string) =
  let uniqueIndex =
   input.ToCharArray()
   |> Array.windowed windowSize
   |> Array.map(isUnique windowSize)
   |> Array.findIndex (fun x -> x = true)
    
  uniqueIndex + windowSize
