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

namespace CenterCLR

open System
open CenterCLR

[<AutoOpen>]
module OptionalBuilderModule =

    // Optional computation builder.
    type OptionalBuilder () =
        // Transfer return
        member this.Return (value: 'T) = Optional.Return(value)
        // Transfer bind
        member this.Bind(optional: Optional<'T>, binder: 'T -> Optional<'U>) =
            optional.Bind<'U>(new Func<_, _>(binder))

    // Now, we can use "optional { ... }" computation block.
    let optional = OptionalBuilder()

