using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class QuestionAnswerViewModel
    {

        public int QuestionId { get; set; }
        public string QuestionDesc { get; set; }
        public string UserNameQ { get; set; }
        public DateTime CreateDataQ { get; set; }

        public ShowAnswerViewModel showAnswer { get; set; }


    }


  public class ShowAnswerViewModel
    {
        public int? AnswerId { get; set; }

        public string? AnswerDesc { get; set; }
        public string? UserNameA { get; set; }
        public DateTime? CreateDataA { get; set; }

    }
}
