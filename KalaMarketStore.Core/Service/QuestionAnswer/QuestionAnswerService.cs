using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Comments_Q_A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.QuestionAnswer
{
    public class QuestionAnswerService : IQuestionAnswerService
    {

        private readonly AppDbContext context;
        public QuestionAnswerService(AppDbContext context) 
        {
            this.context = context;
        }


        public List<QuestionAnswerViewModel>ShowAllQA(int productid)
        {
            var question=context.Questions.Where(x=>x.IsActive &&  x.ProductId == productid);


            List<QuestionAnswerViewModel> showQA = (from Ques in question
                                                    join userQ in context.Users on Ques.UserId equals userQ.UserId

                                                    join ans in context.Answers on Ques.QuestionId equals ans.QuestionId into QA
                                                    from ans in QA.DefaultIfEmpty()

                                                    join userA in context.Users on ans.UserId equals userA.UserId into U
                                                    from userA in U.DefaultIfEmpty()

                                                    select new QuestionAnswerViewModel
                                                    {
                                                        CreateDataQ = Ques.CreateDate,
                                                        QuestionId= Ques.QuestionId,
                                                        QuestionDesc= Ques.QuestionDescription,
                                                        UserNameQ= userQ.UserName+""+ userQ.UserFamily,
                                                        showAnswer=new ShowAnswerViewModel
                                                        {
                                                            AnswerId=ans.AnswerId,
                                                            AnswerDesc=ans.AnswerDescription,
                                                            CreateDataA=ans.CreateDate,
                                                            UserNameA=userA.UserName+""+ userA.UserFamily,   
                                                        }


                                                    }).ToList();
            return showQA;



        }

        public int AddQuestion(Question question)
        {
            try
            {
                context.Questions.Add(question);
                context.SaveChanges();
                return question.QuestionId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public int AddAnswer( Answer answer)
        {
            try
            {
                context.Answers.Add(answer);
                context.SaveChanges();
                return answer.AnswerId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

    }


}
