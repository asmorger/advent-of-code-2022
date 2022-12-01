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

[<Fact>]
let ``Inventory parsing identifies elves`` () =
  let elves = Inventory.parse sample |> Seq.toList
  Assert.Equal(5, elves.Length)
  

[<Fact>]
let ``Elves can find the elf with the greatest load`` () =
  let elves = Inventory.parse sample
  let elf = Elf.findLargestInventory elves
  Assert.Equal(24000, elf.TotalLoad)
  
[<Fact>]
let ``Elves can find greatest load of any 3 elves`` () =
  let elves = Inventory.parse sample
  let totalLoad = Elf.findInventoryForTopN elves 3
  Assert.Equal(45000, totalLoad)
  