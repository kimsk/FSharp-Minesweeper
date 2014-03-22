namespace Minesweeper


module Board =
    open System

    let r = new Random()

    type Tile =
        | Empty
        | Mine

    let getMinePositions num gridSize =
        let rec getRandomPositions (positions:(int*int) list) =
            let newPos = r.Next(fst(gridSize)), r.Next(snd(gridSize))

            let newPositions = 
                if positions |> List.exists ((=)newPos) then
                    positions
                else
                    newPos::positions

            if newPositions |> List.length = num then newPositions
            else getRandomPositions newPositions
        
        getRandomPositions []

    let createNewBoard mineNum gridSize =
        let mines = getMinePositions mineNum gridSize
        [|
            for row in 0..fst(gridSize)-1 ->
                [|
                    for col in 0..snd(gridSize)-1 ->
                        if mines |> List.exists ((=)(row,col)) then
                            Mine
                        else
                            Empty
                |]
        |]

    let toStr (board:Tile[][]) =
        let boardSize = (board.Length, board.[0].Length)

        let line = [1..snd(boardSize)] |> List.fold (fun acc _ -> acc + "---+") "+"

        let printTile tile =
            match tile with
            | Empty -> "   "
            | Mine -> " X "

        let getRowStr row = row |> Array.fold (fun acc t -> acc + (printTile t) + "|") "|"
        let boardStr = board |> Array.fold (fun acc r -> acc + (getRowStr r) + "\r\n") ""
            
        line + "\r\n" + boardStr + line + "\r\n"       
