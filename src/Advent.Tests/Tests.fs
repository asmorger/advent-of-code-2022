namespace Tests

open Advent.Domain
open Xunit
open FsUnit.Xunit

module Day01 =

  let sample =
    """
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

  let inventory = Inventory sample
  let party = Party.parse inventory

  [<Fact>]
  let ``Inventory parsing identifies elves`` () =
    let elves = party
    Assert.Equal(5, elves.Members.Length)


  [<Fact>]
  let ``Elves can find the elf with the greatest load`` () =
    let elf = party.mostSnacks ()
    Assert.Equal(24000, elf.Snacks.TotalCaloricValue)

  [<Fact>]
  let ``Elves can find greatest load of any 3 elves`` () =
    let totalLoad = party.maxSnacksAcrossMultipleElves ()
    Assert.Equal(45000, totalLoad)

module Day03 =

  let input =
    [ "vJrwpWtwJgWrhcsFMMfFFhFp"
      "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"
      "PmmdzqPrVvPwwTWBwg"
      "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFnO"
      "ttgJtRGJQctTZtZT"
      "CrZsJsPPZsGzwwsLwLmpwMDw" ]

  [<Fact>]
  let ``Rucksack priority`` () =
    let priority  = Rucksack.calculatePriority input
    priority |> should equal 157

  [<Fact>]
  let ``Rucksack calculates total group priority`` () =
    let priority = Rucksack.calculateGroupPriority input
    priority |> should equal 70
