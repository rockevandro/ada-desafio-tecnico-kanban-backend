﻿namespace Ada.Kanban.Db.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }
        public string? Lista { get; set; }
    }
}
