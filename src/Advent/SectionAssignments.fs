module Advent.SectionAssignments

open System

type Assignment = int * int

let doAssignmentsEnvelop (assignments: Assignment * Assignment) =
  let x, y = assignments
  
  let isEnveloped(assignments: Assignment * Assignment) =
    let x, y = assignments
    let x1, x2 = x
    let y1, y2 = y
    
    y1 <= x1 && y2 >= x2
  
  isEnveloped (x, y) || isEnveloped (y, x)
  
let doAssignmentsIntersect (assignments: Assignment * Assignment) =
  let x, y = assignments
  let x1, x2 = x
  let y1, y2 = y
  
  let left = seq {x1 .. 1 .. x2 } |> Set
  let right = seq {y1 .. 1 .. y2} |> Set
  
  let intersection = Set.intersect left right
  intersection.Count > 0

type AssignmentPair =
  { IsEnveloped: bool
    HasIntersection: bool }

  static member private parseAssignment(input: string) =
    input.Split('-')
    |> Array.map Int32.Parse
    |> Array.pairwise
    |> Array.head
    |> Assignment


  static member parse(input: string) =
    let pair =
      input.Split(',')
      |> Seq.map AssignmentPair.parseAssignment
      |> Seq.pairwise
      |> Seq.head
    
    let enveloped = doAssignmentsEnvelop pair
    let intersect = doAssignmentsIntersect pair

    { IsEnveloped = enveloped
      HasIntersection = intersect }


type SectionAssignment =
  | SectionAssignment of string seq

  member x.parse =
    let (SectionAssignment value) = x
    value |> Seq.map AssignmentPair.parse

  member x.numberOfEnvelopments =
    x.parse |> Seq.filter (fun x -> x.IsEnveloped) |> Seq.toList |> List.length


  member x.numberOfIntersections =
    x.parse |> Seq.filter (fun x -> x.HasIntersection) |> Seq.toList |> List.length
