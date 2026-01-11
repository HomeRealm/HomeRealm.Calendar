# PowerShell script to create 10 chores via the API

$url = "https://localhost:7117/api/chores"  # Change port if needed

for ($i = 1; $i -le 10; $i++) {
    $now = Get-Date -AsUTC
    $due = $now.AddDays((Get-Random -Minimum 1 -Maximum 14))
    $body = @{
        Name = "Chore $i"
        Description = "Auto-generated chore number $i"
        CreatedAt = $now.ToString("o")
        DueDate = $due.ToString("o")
    } | ConvertTo-Json

    try {
        $response = Invoke-RestMethod -Uri $url -Method Post -Body $body -ContentType "application/json"
        Write-Host "Created chore {$i}: $($response.Name) (ID: $($response.Id))"
    } catch {
        Write-Host "Failed to create chore {$i}: $($_.Exception.Message)"
    }
}