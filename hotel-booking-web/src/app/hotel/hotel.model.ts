export interface Hotel {
    id: number | undefined;
    name: string;
    address: string;
    emailAddress: string;
    phone: string;
    employeesNumber: number;
    image: string;
    hotelDetailsId?: number;
}