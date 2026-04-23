namespace ToDoList
{
    class ToDoItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int Priority { get; set; }

        public ToDoItem(string title, string description, int priority = 1)
        {
            Title = title;
            Description = description;
            Priority = priority;
            IsDone = false;
        }

        public void MarkAsDone()
        {
            IsDone = true;
        }

        public override string ToString()
        {
            char completeMarker = IsDone ? 'X' : ' ';

            if (Description != "")
                return $"[{completeMarker}] {Title, -25} | {Description}";
            else
                return $"[{completeMarker}] {Title}";
        }
    }
}