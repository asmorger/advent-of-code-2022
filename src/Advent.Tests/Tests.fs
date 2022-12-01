module Tests

open System
open Advent.Domain
open Xunit

[<Fact>]
let ``Inventory parsing identifies elves`` () =

  let sample = """
  1000
  2000
  3000

  4000

  5000
  6000

  7000
  8000
  9000

  10000
  """
  let elves = Inventory.parse sample |> Seq.toList
  Assert.True(elves.Length = 5)