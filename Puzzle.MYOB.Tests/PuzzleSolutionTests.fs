module Puzzle.MYOB.PuzzleSolutionTests

open FsUnit
open NUnit.Framework
open Puzzle.MYOB
open System
open System.Diagnostics
open FsCheck
open Puzzle.MYOB.CSharp;

let execSol1 n k = (n, [ 1 .. k ]) ||> EliminatorSol1.getOrderOfRemovals
let execSol2 n k = (n, [| 1 .. k |]) ||> EliminatorSol2.getOrderOfRemovals
            
let getElapsedTime f = let sw = new Stopwatch()
                       sw.Start()
                       f()
                       sw.Stop()        
                       sw.Elapsed

[<Test>]
let ``Test 1: Given a list when k = 4 and n = 1 the order from winner to first eliminated should be 4,3,2,1``() =
            (1, [ 1 .. 4 ]) ||> EliminatorSol1.getOrderOfRemovals |> should equal [4;3;2;1] 
            (1, [| 1 .. 4 |]) ||> EliminatorSol2.getOrderOfRemovals |> should equal [|4;3;2;1|]
            (1, [| 1 .. 4 |]) |> EliminatorSol3.getOrderOfRemovals |> should equal [|4;3;2;1|]
            
[<Test>]
let ``Test 2: Given a list when k = 4 and n = 2 the order of from winner to first elminated should be 1,3,4,2``() =            
            (2, [ 1 .. 4 ]) ||> EliminatorSol1.getOrderOfRemovals |> should equal [1;3;4;2]
            (2, [| 1 .. 4 |]) ||> EliminatorSol2.getOrderOfRemovals |> should equal [|1;3;4;2|]
            (2, [| 1 .. 4 |]) |> EliminatorSol3.getOrderOfRemovals |> should equal [|1;3;4;2|]
[<Test>]
let ``Test 3: When the input array has one item the output array should have one element``() =
            (10, [1]) ||> EliminatorSol1.getOrderOfRemovals |> should equal [1] 
            (10, [|1|]) ||> EliminatorSol2.getOrderOfRemovals |> should equal [|1|]
            (10, [|1|]) |> EliminatorSol3.getOrderOfRemovals  |> should equal [|1|]

[<Test>]
let ``Test 4: When the input array is empty the output array should be empty``() =
            (10, List.Empty) ||> EliminatorSol1.getOrderOfRemovals |> should equal [] 
            (10, Array.empty) ||> EliminatorSol2.getOrderOfRemovals  |> should equal [||]
            (10, Array.empty) |> EliminatorSol3.getOrderOfRemovals |> should equal [||]

[<Test>]
let ``Test 5: When n is less then 1 than the result should be empty``() =
            (-1, [ 0 .. 20 ]) ||> EliminatorSol1.getOrderOfRemovals  |> should equal [] 
            (-3, [| 0 .. 30 |]) ||> EliminatorSol2.getOrderOfRemovals |> should equal [||]
            (-3, [| 0 .. 30 |]) |> EliminatorSol3.getOrderOfRemovals |> should equal [||]

[<Test>]
let ``Test 6: When n equals 1 the result should be a reversed list``() =          
        let revList k = [ 1 .. k ] |> EliminatorSol1.getOrderOfRemovals 1 |> List.rev = [ 1 .. k ]
        [ 1 .. 20 ] |> Seq.iter (fun k -> revList k |> should equal true)
                                                       
[<Test>]
let ``Test 7: Generate 1000 random k and n test cases and verify solution 1 result equals solution 2 result``() = 
            let execTest n k = let r1 = EliminatorSol1.getOrderOfRemovals n [ 1 .. k ] |> Seq.toArray
                               let r2 = EliminatorSol2.getOrderOfRemovals n [| 1 .. k |] 
                               let r3 = EliminatorSol3.getOrderOfRemovals(n, [| 1 .. k |])
                               r1 |> should equal r2
                               r2 |> should equal r3
            Check.Quick execTest                                                                           
    
[<Test>]
let ``Test 8: Create 1000 test cases where k is 1000 and n = 100 and verify that solution 1 equals solution 2``() = 
            [ 1 .. 1000 ] |> Seq.iter(fun i -> let r1 = EliminatorSol1.getOrderOfRemovals 4 [ 1 .. i ] |> Seq.toArray
                                               let r2 = EliminatorSol2.getOrderOfRemovals 4 [| 1 .. i |] 
                                               let r3 = EliminatorSol3.getOrderOfRemovals(4, [| 1 .. i |]) 
                                               r1 |> should equal r2
                                               r3 |> should equal r3)
                                                        
[<Test>]
let ``Test 9: Verify solution 1 execution times are under a second for randomly generated n and k``() =
            let execTest n k = (fun () -> (n, [ 1 .. k ]) ||> EliminatorSol1.getOrderOfRemovals |> ignore) 
                                          |> getElapsedTime |> (fun ts -> ts.Seconds |> should lessThan 1)                                
            Check.Quick execTest                                                                                      
            
[<Test>]
let ``Test 10: Verify solution 2 execution times are under a second for randomly generated n and k``() =
            let execTest n k = (fun () -> (n, [| 1 .. k |]) ||> EliminatorSol2.getOrderOfRemovals |> ignore) 
                                          |> getElapsedTime |> (fun ts -> ts.Seconds |> should lessThan 1) 
            Check.Quick execTest                           
                                                                                                              
[<Test>]
let ``Test 11: Verify solution 3 execution times are under a second for randomly generated n and k``() =
            let execTest n k = (fun () -> (n, [| 1 .. k |]) |> EliminatorSol3.getOrderOfRemovals |> ignore) 
                                          |> getElapsedTime |> (fun ts -> ts.Seconds |> should lessThan 1)                                
            Check.Quick execTest                                                                                                  
            