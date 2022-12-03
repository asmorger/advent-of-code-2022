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
    let priority = Rucksack.calculatePriority loadInput
    printfn $"The total priority of misplaced items is %i{priority}"

    0

  override this.part2() =
    let priority = Rucksack.calculateGroupPriority loadInput

    printfn $"The group priority of misplaced items is %i{priority}"
    0
