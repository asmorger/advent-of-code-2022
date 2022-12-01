module Tests

open Advent.Domain
open Xunit

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

let inventory = Inventory sample
let party = Party.parse inventory

[<Fact>]
let ``Inventory parsing identifies elves`` () =
  let elves = party 
  Assert.Equal(5, elves.Members.Length)
  

[<Fact>]
let ``Elves can find the elf with the greatest load`` () =
  let elf = party.mostSnacks()
  Assert.Equal(24000, elf.Snacks.TotalCaloricValue)
  
[<Fact>]
let ``Elves can find greatest load of any 3 elves`` () =
  let totalLoad = party.maxSnacksAcrossMultipleElves()
  Assert.Equal(45000, totalLoad)
  