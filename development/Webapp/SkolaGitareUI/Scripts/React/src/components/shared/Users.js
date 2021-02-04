import React from "react";
import PageWrapper from "../shared/PageWrapper";
import ContentWraper from "../shared/ContentWraper";
import UserTabs from './Users/UserTabs';


const Users = (props) => {
    return (
        <PageWrapper>
            <ContentWraper title={"KORISNICI"}>
                <UserTabs accessToken={props.accessToken} user={props.user}/>
            </ContentWraper>
        </PageWrapper>

    )
};

export default Users;