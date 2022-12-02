module Advent.Domain

open System

type Snack =
  | Snack of int

  member x.Value = let (Snack value) = x in value

type Snacks =
  { Items: Snack list
    TotalCaloricValue: int }

type Elf = { Snacks: Snacks }

type Inventory =
  | Inventory of string

  member x.readByElf = let (Inventory value) = x in value.Split("\n\n")

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
      for elf in inventory.readByElf do
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
