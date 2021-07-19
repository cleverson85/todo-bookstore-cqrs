using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ToDo.Domain.Core.Events;

namespace ToDo.Application.EventSourcedNormalizers
{
    public class BookHistory
    {
        public static IList<BookHistoryData> HistoryData { get; set; }

        public static IList<BookHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<BookHistoryData>();
            CustomerHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<BookHistoryData>();
            var last = new BookHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new BookHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Title = string.IsNullOrWhiteSpace(change.Title) || change.Title == last.Title
                        ? ""
                        : change.Title,
                    Author = string.IsNullOrWhiteSpace(change.Author) || change.Author == last.Author
                        ? ""
                        : change.Author,
                    Available = last.Available,

                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<BookHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "CreatedEvent":
                        historyData.Action = "Created";
                        historyData.Who = e.User;
                        break;
                    case "UpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "DeletedEvent":
                        historyData.Action = "Deleted";
                        historyData.Who = e.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = e.User ?? "Anonymous";
                        break;

                }

                HistoryData.Add(historyData);
            }
        }
    }
}
