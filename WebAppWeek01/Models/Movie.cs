﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppWeek01.Models
{
    public class Movie
    {   
        public int Id { get; set; }
        public string Title { get; set; }
        [Column(TypeName = ("varchar(30)"))]
        public string? Languege { get; set; }
        [Column(TypeName = ("varchar(30)"))]
        public string? Type { get; set; }
        [Column(TypeName = ("varchar(500)"))]
        public string? Description { get; set; }
        public DateTime? Released { get; set; }
        [Column(TypeName = ("varchar(30)"))]
        public string? Duration { get; set; }
        [Column(TypeName = ("nvarchar(30)"))]
        public string? Country { get; set; }
        [Column(TypeName = ("varchar(40)"))]
        public string? Production { get; set; }
        public int? AgeLimited { get; set; }

        public string? FilePath { get; set; }
        public string? MovieCover { get; set; }

        public ICollection<Person> People { get; set;}
    }
}
