
public record CreateMessagingLogDto(
    string FromPhoneNumber,
    string ToPhoneNumber,
    string MessageContent,
    string MessageReponse,
    int BussinesId
);
