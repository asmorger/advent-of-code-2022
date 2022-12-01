module Advent.Domain

open System

type Food = Food of int

type Elf = {
  Load: Food list
  TotalLoad: int
}

module Elf =
  let findLargestInventory (elves: Elf seq) =
    elves
    |> Seq.sortByDescending(fun x -> x.TotalLoad)
    |> Seq.head
    
  let findInventoryForTopN (elves: Elf seq) (count: int) =
    elves
    |> Seq.sortByDescending(fun x -> x.TotalLoad)
    |> Seq.take count
    |> Seq.sumBy(fun x -> x.TotalLoad)

type Inventory = list<Food> list
module Inventory =
  let parse (input: string) =
    let byElf = input.Split("\n\n")
    
    seq {
      for elf in byElf do
        let load = elf.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
        let caloriesByItem = load |> Seq.map(Int32.Parse)
        let food = caloriesByItem |> Seq.map(Food)
                   
        yield {
          Load = food |> Seq.toList
          TotalLoad = caloriesByItem |> Seq.sum
        }
    }
