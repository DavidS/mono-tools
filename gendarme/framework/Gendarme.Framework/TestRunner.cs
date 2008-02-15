// 
// Gendarme.Framework.ITypeRule interface
//
// Authors:
//	Sebastien Pouliot <sebastien@ximian.com>
//
// Copyright (C) 2008 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Mono.Cecil;

namespace Gendarme.Framework {

	// need to be moved to another assembly dedicated to Gendarme unit 
	// tests where Gendarme.Framework.dll internals are visible

	// FIXME: http://code.google.com/p/google-highly-open-participation-mono/issues/detail?id=75

	/// <summary>
	/// To execute properly Gendarme.Framework.Runner keeps the state of
	/// two variables internally, the current I[Assembly|Type|Method]Rule
	/// and the current target (IMetadataTokenProvider to match the 
	/// [Assembly|Module|Type|Method]Definition being analyzed). This 
	/// class emulate this behavior and also reset the Defects count 
	/// before each Check[Assembly|Type|Method] calls so we can easily
	/// Assert on Defects.Count.
	/// </summary>
	public class TestRunner : Runner {

		public TestRunner (IRule rule)
		{
			CurrentRule = rule;
			CurrentRule.Initialize (this);
		}

		public RuleResult CheckAssembly (AssemblyDefinition assembly)
		{
			Reset ();
			CurrentTarget = assembly;
			return (CurrentRule as IAssemblyRule).CheckAssembly (assembly);
		}

		public RuleResult CheckType (TypeDefinition type)
		{
			Reset ();
			CurrentTarget = type;
			return (CurrentRule as ITypeRule).CheckType (type);
		}

		public RuleResult CheckMethod (MethodDefinition method)
		{
			Reset ();
			CurrentTarget = method;
			return (CurrentRule as IMethodRule).CheckMethod (method);
		}
	}
}
