﻿namespace SurveyBasket.Api.Contracts.Responses;

public record PollResponse (
 int Id,
 string Title,
 string Summary,
 bool IsPublished,
 DateOnly StartAt,
 DateOnly EndAt

    );

