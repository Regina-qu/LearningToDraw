//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Рисовалка.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Images
    {
        public int ID { get; set; }
        public int IDImagesType { get; set; }
        public string ImagePath { get; set; }
    
        public virtual ImageType ImageType { get; set; }
    }
}
