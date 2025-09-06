
export interface IndustryDto{

    id: number;
    name: string;

}

export interface CompanyDto{

    name: string;
    industryId: number;

}

export interface SummaryStepDto{

    termsofServiceAccepted: boolean;
    privacyPolicyAccepted: boolean;

}

export interface UserDto{

    name: string;
    firstName: string;
    userName: string;
    password: string;
    passwordRepeat: string;
    email?:string | null;
    
}

export interface RegistrationRequest{

    company: CompanyDto;
    user: UserDto;
    summary: SummaryStepDto;
}

export interface RegistrationResponse{

    success: boolean;
    message: string;

}