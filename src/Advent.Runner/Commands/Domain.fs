module Advent.Runner.Commands.Domain

open Spectre.Console
open Spectre.Console.Cli

type DailySettings() =
  inherit CommandSettings()


[<AbstractClass>]
type DailyCommand() =
  inherit Command<DailySettings>()

  abstract member part1: unit -> int
  abstract member part2: unit -> int

  override this.Execute(_, settings) =
    let prompt = SelectionPrompt<int>()
    prompt.Title <-  "Which part do you want to run?"
    prompt.AddChoices([ 1; 2 ]) |> ignore
    
    let part = AnsiConsole.Prompt prompt
    
    match part with
    | 1 ->  this.part1()
    | 2 ->  this.part2()
    | _ -> failwith "bad input"
    
