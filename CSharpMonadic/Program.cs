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

using System;
using System.Linq;

namespace CenterCLR
{
    class Program
    {
        static void Main(string[] args)
        {
            ////////////////////////////////////////////////////////////////////
            // Option (Maybe) monad implementations.

            var option11 = Optional.Return(1);
            var option12 = option11.Bind(value1 =>
            {
                var option21 = Optional.Return(2);
                return option21.Bind(value2 =>
                {
                    var option31 = Optional.Return(3);
                    return option31.Bind(value3 => Optional.Return(value1 + value2 + value3));
                });
            });
            option12.Match(Console.WriteLine, () => { });

            ////////////////////////////////////////////////////////////////////
            // LINQ expressions maybe monadic?

            var list11 = new[] {1};
            var list12 = list11.SelectMany(value1 =>
            {
                var list21 = new[] {2};
                return list21.SelectMany(value2 =>
                {
                    var list31 = new[] {3};
                    return list31.SelectMany(value3 => new[] {value1 + value2 + value3});
                });
            });
            foreach (var value in list12) Console.WriteLine(value);

            ////////////////////////////////////////////////////////////////////
            // Option monad acts by LINQ query expressions.

            var optionResult =
                from value1 in Optional.Return(1)
                from value2 in Optional.Return(2)
                from value3 in Optional.Return(3)
                select value1 + value2 + value3;
            optionResult.Match(Console.WriteLine, () => { });
        }
    }
}
