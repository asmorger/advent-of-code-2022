module Advent.SectionAssignments

open System

type Range =
  { Beginning: int
    End: int }

  member x.isEnveloped(other: Range) =
    other.Beginning <= x.Beginning && other.End >= x.End

  member x.isIntersected(y: Range) =
    let left = seq { x.Beginning .. 1 .. x.End } |> Set
    let right = seq { y.Beginning .. 1 .. y.End } |> Set
    
    let intersection = Set.intersect left right
    intersection.Count > 0

type AssignmentPair =
  { IsFullyContained: bool
    HasAnyOverlap: bool }

  static member private parseIndividual(input: string) =
    let value = input.Split('-') |> Array.map Int32.Parse |> Array.pairwise
    let x, y = value[0]

    { Beginning = x; End = y }

  static member private compare(input: Range * Range) =
    let x, y = input
    x.isEnveloped y || y.isEnveloped x


  static member private intersect(input: Range * Range) =
    let x, y = input
    x.isIntersected y || y.isIntersected x

  static member parse(input: string) =
    let pairs = input.Split(',')
    let first = AssignmentPair.parseIndividual pairs[0]
    let second = AssignmentPair.parseIndividual pairs[1]
    let compare = AssignmentPair.compare (first, second)
    let anyOverlap = AssignmentPair.intersect (first, second)

    { IsFullyContained = compare
      HasAnyOverlap = anyOverlap }


type SectionAssignment =
  | SectionAssignment of string seq

  member x.parse =
    let (SectionAssignment value) = x
    value |> Seq.map AssignmentPair.parse

  member x.numberOfEnvelopments =
    x.parse |> Seq.filter (fun x -> x.IsFullyContained) |> Seq.toList |> List.length


  member x.numberOfIntersections =
    x.parse |> Seq.filter (fun x -> x.HasAnyOverlap) |> Seq.toList |> List.length
