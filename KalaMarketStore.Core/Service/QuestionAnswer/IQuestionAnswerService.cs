using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities.Comments_Q_A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.QuestionAnswer
{
    public interface IQuestionAnswerService
    {
        List<QuestionAnswerViewModel> ShowAllQA(int productid);

        int AddQuestion(Question question);
        int AddAnswer(Answer answer);
    }
}
