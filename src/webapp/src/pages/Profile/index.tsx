import React from "react";
import { Card, CardContent, CardHeader, CardTitle } from "../../components/card";

const ProfilePage: React.FC = () => {
    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Profile</CardTitle>
                </CardHeader>
                <CardContent>
                    <p>User profile information will be displayed here.</p>
                </CardContent>
            </Card>
        </div>
    );
};

export default ProfilePage;