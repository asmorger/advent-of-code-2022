open Advent.Runner.Commands.Day01
open Advent.Runner.Commands.Day02
open Advent.Runner.Commands.Domain
open Spectre.Console.Cli

[<EntryPoint>]
let main argv =
  let app = CommandApp()

  app.Configure(fun c ->
    c.AddBranch(
      "day",
      fun (day: IConfigurator<DailySettings>) ->
        day.AddCommand<Day01>("one") |> ignore
        day.AddCommand<Day02>("two") |> ignore
    ))

  app.Run(argv)
