
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

export interface LoginRequest{

    userName: string;
    password: string;

}

export interface LoginResponse{

    success: boolean;
    message: string;
    token?: string;
    expiresAtUtc?: string;
    userName?: string;
    email?: string | null;
    name?: string;
    firstName?: string;
    companyId?: number;

}