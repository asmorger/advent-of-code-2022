module Advent.Runner.Commands.Day03

open System.IO
open Advent.Domain
open Advent.Runner.Commands.Domain

type Day03() =
  inherit DailyCommand()

  let loadInput =
    let path = Directory.GetCurrentDirectory() + "/Resources/Day03-Source.txt"
    let input = File.ReadLines path
    input

  override this.part1() =
    let rucksacks = Rucksack.parse loadInput
    let priority = rucksacks |> Seq.sumBy (fun x -> x.mispackagedItem.Priority)
    printfn $"The total priority of misplaced items is %i{priority}"

    0

  override this.part2() =
    let rucksacks = Rucksack.parse loadInput
    let groups = rucksacks |> Seq.chunkBySize 3
    let priority = groups |> Seq.map (Rucksack.group) |> Seq.sumBy(fun x -> x.Priority)

    printfn $"The group priority of misplaced items is %i{priority}"
    0
