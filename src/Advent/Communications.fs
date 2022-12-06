module Advent.Communications

let isUnique windowSize (input: char array) =
  let set = Set input
  set.Count = windowSize

let determineFirstUniqueIndex (windowSize: int) (input: string) =
  let windows = input.ToCharArray() |> Array.windowed windowSize

  let uniqueIndex =
    windows |> Array.map(isUnique windowSize)  |> Array.findIndex (fun x -> x = true)
    
  uniqueIndex + windowSize
