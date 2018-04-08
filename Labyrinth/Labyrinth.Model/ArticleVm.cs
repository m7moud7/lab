using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Labyrinth.Model
{
    public class ArticleVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل القسم")]
        public int SecID { get; set; }

        [Display(Name = "بقلم"), MaxLength(200, ErrorMessage = "اقصى عدد احرف 200")]
        public string WrittenBy { get; set; }

        [Display(Name = "العنوان الرئيسى"), MaxLength(210, ErrorMessage = "اقصى عدد احرف 210"), Required(ErrorMessage = "من فضلك ادخل العنوان")]
        public string Title { get; set; }

        [Display(Name = "العنوان الفرعى"), MaxLength(210, ErrorMessage = "اقصى عدد احرف 210")]
        public string SubTitle { get; set; }

        [Display(Name = "العنوان الفرعى الاول")]
        public string SubTitle1 { get; set; }

        [Display(Name = "العنوان الفرعى الثانى")]
        public string SubTitle2 { get; set; }

        [Display(Name = "العنوان الفرعى الثالث")]
        public string SubTitle3 { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل  التاجات"), Display(Name = "التاجز")]
        public string Tags { get; set; }

        [Display(Name = "الكلمات المفتاحية")]
        public string Keywords { get; set; }

        [Display(Name = "الروابط المتعلقة")]
        public string RelatedLinks { get; set; }

        [Display(Name = "الاخبار المتعلقة")]
        public string RelatedNews { get; set; }
        public string[] SelectedRelatedNews { get; set; }
        public List<RelatedNewsVM> RelatedNewsList { get; set; }

        [Display(Name = "الملخص"), Required(ErrorMessage = "من فضلك ادخل الملخص")]
        public string Brief { get; set; }

        [Display(Name = "الخبر"), Required(ErrorMessage = "من فضلك ادخل  متن الخبر")]
        public string Story { get; set; }

        [Display(Name = "ملاحظات")]
        public string Notes { get; set; }

        public int CUser { get; set; }

        public DateTime CDate { get; set; }

        [Display(Name = "نوع الخبر")]
        public int Type { get; set; }

        public string SchdeuledPublish { get; set; }

        [Display(Name = "نشر")]
        public bool IsApproved { get; set; }

        [Display(Name = "الكاتب")]
        public int? EditorID { get; set; }

        [Display(Name = "تمرير اللى")]
        public int? Status { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "توب ستورى")]
        public bool IsTop { get; set; }

        [Display(Name = "اظهار فى السكشن")]
        public bool IsSection { get; set; }

        [Display(Name = "اظهار فى الرئيسيه")]
        public bool IsHome { get; set; }

        [Display(Name = "اضافة للأخبار العاجلة")]
        public bool IsTicker { get; set; }

        [Display(Name = "حذف الخبر")]
        public bool IsDeleted { get; set; }

        public Int64? AttachmentID { get; set; }

        [Display(Name = "تعليق الصورة"), MaxLength(210, ErrorMessage = "اقصى عدد احرف 210")]
        public string ImageCaption { get; set; }

        public string ImagePath { get; set; }

        public int? CurrentGroupID { get; set; }
        public int? CurrentUserID { get; set; }


        public Int64? CoverID { get; set; }

        //[Required(ErrorMessage = "من فضلك ادخل تعليق صورة الكافر")]
        public string ImageCaptionCover { get; set; }



        public string Embed { get; set; }

        public int? AlbumID { get; set; }
        public int? GoldID { get; set; }
        public int? CurrencyID { get; set; }


        public List<NewsMetaVM> NewsMeta { get; set; }


        //public List<AlbumVm> Albums { get; set; }
        //public string AlbumList { get; set; }
        //public List<Services.GoldVM> Golds { get; set; }
        //public List<Services.CurrencyVM> Currencys { get; set; }
        //public string GoldList { get; set; }
        //public string CurrencyList { get; set; }
        //public List<PollVm> Polls { get; set; }
        //public string PollList { get; set; }
        //public string[] SelectedPolls { get; set; }
        //public string[] SelectedAlbums { get; set; }
        //public string[] SelectedGolds { get; set; }
        //public string[] SelectedCurrencys { get; set; }

        //public List<AttachmentVM> Images { get; set; }
        //public List<AttachmentVM> WorkFlowAttachment { get; set; }
        //public string SecName { get; set; }
        //public string Date { get; set; }

        //public AttachmentVM ImageDetails { get; set; }
        //public AttachmentVM CoverDetails { get; set; }

        //public string CurrentGroupName { get; set; }
        //public string CurrentUserName { get; set; }

        //public string AttachmentPath { get; set; }
        //public bool FromAddEdit { get; set; }//True From Add Article, False From Edit Article
        //public string Path { get; set; }
        //public int? LastPasserID { get; set; }
        //public string LastPasserName { get; set; }
        //public int? LastGroupID { get; set; }
        //public string LastGroupName { get; set; }
        //public DateTime? LastPasserDate { get; set; }





        //[Display(Name = "صورة ال cover  ")]
        //public AttachmentVM Cover { get; set; }
        //[Display(Name = "تعليق صورة ال cover")]
        //[MaxLength(210, ErrorMessage = "اقصى عدد احرف 210")]
        //public string CoverCaption { get; set; }
        //[Display(Name = "نشر فى")]



    }
}
