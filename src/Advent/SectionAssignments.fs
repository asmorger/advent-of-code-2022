module Advent.SectionAssignments

open System

type Range =
  {
    Beginning: int
    End: int
  }
  member x.isContainedBy (other:Range) =
    other.Beginning <= x.Beginning && other.End >= x.End
    
type AssignmentPair =
  {
    IsFullyContained: bool
  }
  static member private parseIndividual (input:string ) =
    let value = input.Split('-')
                 |> Array.map Int32.Parse
                 |> Array.pairwise
    let x, y = value[0]
    
    {
      Beginning = x
      End = y
    }
 
  static member private compare (input: Range * Range) =
    let x, y = input
    x.isContainedBy y || y.isContainedBy x
    
  static member parse (input:string) =
    let pairs = input.Split(',')
    let first = AssignmentPair.parseIndividual pairs[0]
    let second = AssignmentPair.parseIndividual pairs[1]
    let compare = AssignmentPair.compare (first, second)

    {
      IsFullyContained = compare
    }
    

type SectionAssignment =
  | SectionAssignment of string seq

  member x.parse =
    let (SectionAssignment value) = x
    value |> Seq.map AssignmentPair.parse
    
  member x.numberOfOverlaps =
    x.parse
    |> Seq.filter(fun x -> x.IsFullyContained)
    |> Seq.toList
    |> List.length