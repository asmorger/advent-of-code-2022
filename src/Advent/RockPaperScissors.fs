module Advent.RockPaperScissors

type PlayResult =
  | Win
  | Loss
  | Draw

  member x.Value =
    match x with
    | Win -> 6
    | Loss -> 0
    | Draw -> 3

  static member parse(input: string) =
    match input with
    | "X" -> Loss
    | "Y" -> Draw
    | "Z" -> Win
    | _ -> failwith "unknown"

type Play =
  | Rock
  | Paper
  | Scissors

  member x.Value =
    match x with
    | Rock -> 1
    | Paper -> 2
    | Scissors -> 3

  member x.playResult(other: Play) =
    let set = (x, other)

    match set with
    | Rock, Rock -> Draw
    | Rock, Paper -> Loss
    | Rock, Scissors -> Win
    | Paper, Paper -> Draw
    | Paper, Rock -> Win
    | Paper, Scissors -> Loss
    | Scissors, Scissors -> Draw
    | Scissors, Paper -> Win
    | Scissors, Rock -> Loss

  static member parse(input: string) =
    match input with
    | "A" -> Rock
    | "B" -> Paper
    | "C" -> Scissors
    | "X" -> Rock
    | "Y" -> Paper
    | "Z" -> Scissors
    | _ -> failwith "todo"

  static member determinePlayBasedUponExpectedResult(set: Play * PlayResult) =
    match set with
    | Rock, Win -> Paper
    | Rock, Loss -> Scissors
    | Rock, Draw -> Rock
    | Paper, Win -> Scissors
    | Paper, Loss -> Rock
    | Paper, Draw -> Paper
    | Scissors, Win -> Rock
    | Scissors, Loss -> Paper
    | Scissors, Draw -> Scissors

type Match =
  { Them: Play
    Me: Play }

  member x.Value =
    let pointsForPlay = x.Me.Value
    let pointsForResult = x.Me.playResult x.Them

    let total = pointsForPlay + pointsForResult.Value
    total

  static member parse(input: string seq) =
    seq {
      for line in input do
        let values = line.Split(" ")
        let them = Play.parse values[0]
        let me = Play.parse values[1]

        yield { Them = them; Me = me }
    }

  static member updatedRules(input: string seq) =
    seq {
      for line in input do
        let values = line.Split(" ")
        let them = Play.parse values[0]
        let result = PlayResult.parse values[1]
        let me = Play.determinePlayBasedUponExpectedResult (them, result)

        yield { Them = them; Me = me }
    }

type Tournament =
  { Matches: Match list
    TotalPoints: int }

  static member parse(input: string seq) =
    let matches = Match.parse input
    let total = matches |> Seq.sumBy (fun x -> x.Value)

    { Matches = matches |> Seq.toList
      TotalPoints = total }


  static member parseUpdated(input: string seq) =
    let matches = Match.updatedRules input
    let total = matches |> Seq.sumBy (fun x -> x.Value)

    { Matches = matches |> Seq.toList
      TotalPoints = total }
