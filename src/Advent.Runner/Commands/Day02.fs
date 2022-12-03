module Advent.Runner.Commands.Day02

open Advent.RockPaperScissors
open Advent.Runner.Commands.Domain

type Day02() =
  inherit DailyCommand()

  let loadInput =
    readInputAsSeq "Day02"
    
  let loadTournament =
    let tournament = Tournament.parse loadInput 
    tournament

  override this.part1() =
    let tournament = loadTournament
    printfn $"The total result from the Rock, Paper, Scissors tournament is %i{tournament.TotalPoints}"

  override this.part2() =
    let tournament2 = Tournament.parseUpdated loadInput
    printfn $"The updated total result from the Rock, Paper, Scissors tournament is %i{tournament2.TotalPoints}"
