// For more information see https://aka.ms/fsharp-console-apps
open System.IO
open Advent.Domain

let path = Directory.GetCurrentDirectory() + "/Day01-Source.txt"
let input = File.ReadAllText path

let elves = Inventory.parse input
let largestLoad = Elf.findLargestInventory elves
printf $"The largest calorie carrying Elf is %i{largestLoad.TotalLoad}"

let largestThreeLoads = Elf.findInventoryForTopN elves 3
printf $"The largest calorie count for 3 elves is %i{largestThreeLoads}"