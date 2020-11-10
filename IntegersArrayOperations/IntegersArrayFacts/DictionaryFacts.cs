﻿using Xunit;
using IntegersArray;
using System.Collections.Generic;

namespace IntegersArrayFacts
{
    public class DictionaryFacts
    {
        [Fact]

        public void AddMethodShouldAddNewElement()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(6, "hey");
            Assert.Equal(1, dictionary.Count);
            dictionary.Add(16, "oi!");
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]

        public void AddMethodWithGivenKeyValuePairShouldAddNewElement()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            Assert.Equal(1, dictionary.Count);
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]

        public void IndexerShouldGetTheElementValueWithGivenKey()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            dictionary.Add((new KeyValuePair<int, string>(8, "aii")));
            Assert.Equal(dictionary[6], "hey");
            Assert.Equal(dictionary[8], "aii");
        }
    }
}
