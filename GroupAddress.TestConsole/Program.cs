using GroupAddress.Core;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

namespace GroupAddress.TestConsole
{
    internal class Program
    {


        static void Main(string[] args)
        {

            var f = new Foo();

            f.FooBar = new Bar() { Content = "999" };


        }
    }

    public class Foo
    {
        
        public FooFoo FooFoo { get; set; }

        private Bar _fooBar;
        public Bar FooBar { get => _fooBar; set => _fooBar = value; }


        public Foo()
        {

            FooBar = new Bar() { Content = "3456" };
            FooFoo = new FooFoo(ref _fooBar);


        }
    }

    public class FooFoo
    {
        private Bar _fooFooBar;
        public Bar FooFooBar { get => _fooFooBar; set => _fooFooBar = value; }

        public FooFoo(ref Bar bar)
        {
            _fooFooBar = bar;
        }
    }


    public class Bar
    {
        public string Content { get; set; }
    }

}

