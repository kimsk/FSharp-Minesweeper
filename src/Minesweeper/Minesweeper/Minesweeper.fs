namespace Minesweeper


module Board =
    open System

    let r = new Random()

    type Tile =
        | Empty
        | Mine

    let getMinePositions num gridSize =
        let rec getRandomPositions (positions:Set<int*int>) =
            let newPositions = positions |> Set.add  (r.Next(fst(gridSize)), r.Next(snd(gridSize)))
            if newPositions |> Seq.length = num then 
                newPositions |> List.ofSeq
            else getRandomPositions newPositions
        
        getRandomPositions Set.empty

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
