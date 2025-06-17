using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace kek
{
    class PaperComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var xPaper = x as Paper;
            var yPaper = y as Paper;
            return Compare(xPaper.Author.Surname, yPaper.Author.Surname);
        }
    }
}
