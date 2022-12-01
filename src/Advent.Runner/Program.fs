// For more information see https://aka.ms/fsharp-console-apps
open System.IO
open Advent.Domain

let path = Directory.GetCurrentDirectory() + "/Day01-Source.txt"
let input = File.ReadAllText path |> Inventory

let party = Party.parse input
let largestLoad = party.mostSnacks()
printfn $"The largest calorie carrying Elf is %i{largestLoad.Snacks.TotalCaloricValue}"

let largestThreeLoads = party.maxSnacksAcrossMultipleElves()
printfn $"The largest calorie count for 3 elves is %i{largestThreeLoads}"