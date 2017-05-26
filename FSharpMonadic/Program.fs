/////////////////////////////////////////////////////////////////////////////////////////////////
//
// CSharpMonadic
// Copyright (c) 2017 Kouji Matsui (@kekyo2)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//	http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/////////////////////////////////////////////////////////////////////////////////////////////////

module internal Test =
    open System
    open System.Linq
    open CenterCLR

    let test () = 
        ////////////////////////////////////////////////////////////////////
        // Option (Maybe) monad implementations.

        let option11 = Optional.Return(1)
        let option12 = option11.Bind(fun value1 ->
            let option21 = Optional.Return(2)
            option21.Bind(fun value2 ->
                let option31 = Optional.Return(3)
                option31.Bind(fun value3 -> Optional.Return(value1 + value2 + value3))))

        option12.Match((fun value -> Console.WriteLine(value)), fun _ -> ())

        ////////////////////////////////////////////////////////////////////
        // LINQ expressions maybe monadic?

        let list11 = [|1|];
        let list12 = list11.SelectMany(fun value1 ->
            let list21 = [|2|]
            list21.SelectMany(fun value2 ->
                let list31 = [|3|]
                list31.SelectMany(fun value3 -> [|value1 + value2 + value3|] |> seq)))

        for value in list12 do Console.WriteLine(value) done

        ////////////////////////////////////////////////////////////////////
        // Improvements by F#'s sequence.

        let list41 = [|1|];
        let list42 = list41 |> Seq.collect(fun value1 ->
            let list51 = [|2|]
            list51 |> Seq.collect(fun value2 ->
                let list61 = [|3|]
                list61 |> Seq.collect(fun value3 -> [|value1 + value2 + value3|])))

        for value in list42 do Console.WriteLine(value) done

        ////////////////////////////////////////////////////////////////////
        // Improvements by sequence expression.

        let listResult = seq {
            for value1 in [|1|] do
            for value2 in [|2|] do
            for value3 in [|3|] do
            yield value1 + value2 + value3 
        }

        for value in listResult do Console.WriteLine(value) done

        ////////////////////////////////////////////////////////////////////
        // Option monad acts by computation expressions.

        let optionResult = optional {
            let! value1 = Optional.Return(1)
            let! value2 = Optional.Return(2)
            let! value3 = Optional.Return(3)
            return value1 + value2 + value3
        }

        optionResult.Match((fun value -> Console.WriteLine(value)), fun _ -> ())

[<EntryPoint>]
let main argv =
    Test.test()
    0 // return an integer exit code
