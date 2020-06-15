using System;
using System.Collections.ObjectModel;

namespace issuelist
{
    public static class usr
    {
        public static String autenticacao;
        public static ObservableCollection<Issue> Issues { set; get; }
        public static ObservableCollection<Repos> Repositorios { set; get; }
    }
}
