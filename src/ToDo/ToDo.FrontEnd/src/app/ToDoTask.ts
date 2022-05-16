import { Taxonomy } from "./Taxonomy";

export interface ToDoTask {
  Id: string;
  Name: string;
  Priority: number;
  Status: number;
  Category: Taxonomy;
}