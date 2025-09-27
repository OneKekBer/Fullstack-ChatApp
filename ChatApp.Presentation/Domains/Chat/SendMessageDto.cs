namespace ChatApp.Presentation.Domains.Chat;

public record SendMessageDto(string AuthorLogin, string Message, string ChatName);