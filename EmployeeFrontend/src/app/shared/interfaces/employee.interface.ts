export interface IEmployee {
  id?: string;
  fullName: string;
  email: string;
  salary: number;
  joinedDate: Date;
  createdBy?: string;
  createdOn?: Date;
  lastModifiedBy?: string;
  lastModifiedOn?: Date;
  isDeleted?: boolean;
}
