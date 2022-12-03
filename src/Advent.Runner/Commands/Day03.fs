module Advent.Runner.Commands.Day03

open System.IO
open Advent.Domain
open Advent.Runner.Commands.Domain

type Day03() =
  inherit DailyCommand()

  let loadInput =
    readInputAsSeq "Day03"
    
  override this.part1() =
    let priority = Rucksack.calculatePriority loadInput
    printfn $"The total priority of misplaced items is %i{priority}"

  override this.part2() =
    let priority = Rucksack.calculateGroupPriority loadInput
    printfn $"The group priority of misplaced items is %i{priority}"
