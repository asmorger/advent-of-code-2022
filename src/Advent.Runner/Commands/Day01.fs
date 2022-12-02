module Advent.Runner.Commands.Day01

open System.IO
open Advent.Domain
open Advent.Runner.Commands.Domain

type Day01() =
  inherit DailyCommand()

  let loadParty =
    let path = Directory.GetCurrentDirectory() + "/Resources/Day01-Source.txt"
    let input = File.ReadAllText path |> Inventory
    let party = Party.parse input
    party

  override this.part1() =
    let party = loadParty

    let largestLoad = party.mostSnacks ()
    printfn $"The largest calorie carrying Elf is %i{largestLoad.Snacks.TotalCaloricValue}"
    0

  override this.part2() =
    let party = loadParty
    let largestThreeLoads = party.maxSnacksAcrossMultipleElves()
    printfn $"The largest calorie count for 3 elves is %i{largestThreeLoads}"

    0
