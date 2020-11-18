using System;
using System.Collections.Generic;
using Linq;
using Xunit;

namespace LinqFacts
{
    public class LinqMethodsFacts
    {
        [Fact]
        public void AllMethodShouldReturnTrueIFAllElementsMeetTheGivenCondition()
        {
            var array = new int[3];
            array[0] = 6;
            array[1] = 8;
            array[2] = 10;

            var array1 = new int[2];
            array1[0] = 6;
            array1[1] = 9;

            Assert.True(LinqMethods.All<int>(array, c => c % 2 == 0));
            Assert.False(LinqMethods.All<int>(array1, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.All<int>(null, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.All<int>(array, null));
        }
    }
}