public record SendMessageDto
(
    string AuthToken,
    string AccountSid,
    string FromNumber,
    string ToNumber,
    MessageType MessageType,
    string Message,
    int BusinessId
);