import { TennisMatchDetails } from "./tennisMatchDetails";

export interface TennisEventSuccessResponse
{
  id: string;
  responseObj: TennisMatchDetails;
}