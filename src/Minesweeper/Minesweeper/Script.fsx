// Requirement from https://github.com/NashXam/minesweeper
// A 8x8 grid of tiles, a high score, and a current score.
// Tile types : 
//  Empty: No mines
//  Mine
//  

let gridSize = 8,8
let mineNum = 10

#load @"Minesweeper.fs"
open Minesweeper

let boards = [1..10] |> List.map (fun _ -> Board.createNewBoard mineNum gridSize)

boards |> List.iter (fun b -> printfn "%s" (Board.toStr b))

