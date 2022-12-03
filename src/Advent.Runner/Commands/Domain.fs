module Advent.Runner.Commands.Domain

open System.IO
open Spectre.Console
open Spectre.Console.Cli

let readInput name =
  let path = Directory.GetCurrentDirectory() + $"/Resources/%s{name}.txt"
  let input = File.ReadAllText path
  input


let readInputAsSeq name =
  let path = Directory.GetCurrentDirectory() + $"/Resources/%s{name}.txt"
  let input = File.ReadAllLines path
  input

type DailySettings() =
  inherit CommandSettings()


[<AbstractClass>]
type DailyCommand() =
  inherit Command<DailySettings>()

  abstract member part1: unit -> unit
  abstract member part2: unit -> unit


  override this.Execute(_, settings) =
    let prompt = SelectionPrompt<int>()
    prompt.Title <- "Which part do you want to run?"
    prompt.AddChoices([ 1; 2 ]) |> ignore

    let part = AnsiConsole.Prompt prompt

    match part with
    | 1 -> this.part1 ()
    | 2 -> this.part2 ()
    | _ -> failwith "bad input"
    |> ignore

    0
